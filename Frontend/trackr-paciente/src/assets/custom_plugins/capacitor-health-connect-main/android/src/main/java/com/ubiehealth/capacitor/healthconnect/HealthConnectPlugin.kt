package com.ubiehealth.capacitor.healthconnect


import android.app.Activity
import android.content.Intent
import android.net.Uri
import android.util.Log
import android.os.Build
import androidx.activity.result.ActivityResult
import androidx.activity.result.ActivityResultLauncher
import androidx.activity.result.contract.ActivityResultContract
import androidx.core.app.ActivityCompat
import androidx.health.connect.client.HealthConnectClient
import androidx.health.connect.client.PermissionController
import androidx.health.connect.client.impl.converters.datatype.RECORDS_TYPE_NAME_MAP
import androidx.health.connect.client.permission.HealthPermission
import androidx.health.connect.client.records.Record
import androidx.health.connect.client.records.StepsRecord
import androidx.health.connect.client.records.WeightRecord
import androidx.health.connect.client.records.HeartRateRecord
import androidx.health.connect.client.records.SleepSessionRecord
import androidx.health.connect.client.records.BloodPressureRecord
import androidx.health.connect.client.request.ChangesTokenRequest
import androidx.health.connect.client.request.ReadRecordsRequest
import androidx.lifecycle.lifecycleScope
import com.getcapacitor.JSArray
import com.getcapacitor.JSObject
import com.getcapacitor.Plugin
import com.getcapacitor.PluginCall
import com.getcapacitor.PluginMethod
import com.getcapacitor.annotation.ActivityCallback
import com.getcapacitor.annotation.CapacitorPlugin
import kotlinx.coroutines.flow.flow
import kotlinx.coroutines.flow.toList
import kotlinx.coroutines.launch
import org.json.JSONObject
import kotlin.jvm.internal.Reflection.createKotlinClass
import kotlin.reflect.KClass


@CapacitorPlugin(name = "HealthConnect")
class HealthConnectPlugin : Plugin() {
    //private val healthConnectClient by lazy { HealthConnectClient.getOrCreate(this.context.applicationContext) }
    private val healthConnectClient by lazy { HealthConnectClient.getOrCreate(this.context) }
    private val permissionContract by lazy {
        PermissionController.createRequestPermissionResultContract()
    }

    @PluginMethod
    fun checkAvailability(call: PluginCall) {
        //En caso de que la version actual sea menor a la API 34 (UPSIDE_DOWN_CAKE)(Android 14) entonces la funcionalidad de este plugin se inabilita
        if(Build.VERSION.SDK_INT < Build.VERSION_CODES.UPSIDE_DOWN_CAKE ){
            val res = JSObject().apply {
                put("availability", "Unavailable")
            }
            call.resolve(res)
        } else { //En caso de ser API 34 o mayor continuar normalmente
            val availabilityStatus = HealthConnectClient.getSdkStatus(this.context)

            if (availabilityStatus == HealthConnectClient.SDK_UNAVAILABLE_PROVIDER_UPDATE_REQUIRED) {
                // Optionally redirect to package installer to find a provider, for example:
                val providerPackageName = "com.ubiehealth.capacitor.healthconnect"
                val uriString =
                        "market://details?id=$providerPackageName&url=healthconnect%3A%2F%2Fonboarding"
                context.startActivity(
                        Intent(Intent.ACTION_VIEW).apply {
                            setPackage("com.android.vending")
                            data = Uri.parse(uriString)
                            putExtra("overlay", true)
                            putExtra("callerId", context.packageName)
                        }
                )
                return
            }


            val availability = when (val status = HealthConnectClient.getSdkStatus(this.context)) {
                HealthConnectClient.SDK_AVAILABLE -> "Available"
                HealthConnectClient.SDK_UNAVAILABLE -> "Unavailable"
                HealthConnectClient.SDK_UNAVAILABLE_PROVIDER_UPDATE_REQUIRED -> "NotInstalled"
                else -> throw RuntimeException("Invalid sdk status: $status")
            }

            val res = JSObject().apply {
                put("availability", availability)
            }
            call.resolve(res)
        }
    }

    @PluginMethod
    fun insertRecords(call: PluginCall) {
        this.activity.lifecycleScope.launch {
            val records = call.getArray("records").toList<JSONObject>().map { it.toRecord() }
            val result = healthConnectClient.insertRecords(records)

            val res = JSObject().apply {
                put("recordIds", result.recordIdsList)
            }
            call.resolve(res)
        }
    }

    @PluginMethod
    fun readRecord(call: PluginCall) {
        this.activity.lifecycleScope.launch {
            val type = call.getString("type").let {
                RECORDS_TYPE_NAME_MAP[it] ?: throw IllegalArgumentException("Unexpected RecordType: $it")
            }

            val result = healthConnectClient.readRecord(
                    recordType = type,
                    recordId = requireNotNull(call.getString("recordId"))
            )

            val res = JSObject().apply {
                this.put("record", result.record.toJSONObject())
            }
            call.resolve(res)
        }
    }

    @PluginMethod
    fun readRecords(call: PluginCall) {
        this.activity.lifecycleScope.launch {
            val type = call.getString("type").let {
                RECORDS_TYPE_NAME_MAP[it] ?: throw IllegalArgumentException("Unexpected RecordType: $it")
            }
            val request = ReadRecordsRequest(
                    recordType = type,
                    timeRangeFilter = call.data.getTimeRangeFilter("timeRangeFilter"),
                    dataOriginFilter = call.data.getDataOriginFilter("dataOriginFilter"),
                    ascendingOrder = call.getBoolean("ascendingOrder") ?: true,
                    pageSize = call.getInt("pageSize") ?: 1000,
                    pageToken = call.getString("pageToken"),
            )
            val result = healthConnectClient.readRecords(request)

            val res = JSObject().apply {
                val records = result.records.map { it.toJSONObject() }.toJSONArray()
                this.put("records", records)
                this.put("pageToken", result.pageToken)
            }
            call.resolve(res)
        }
    }

    @PluginMethod
    fun getChangesToken(call: PluginCall) {
        this.activity.lifecycleScope.launch {
            val types = call.getArray("types").toList<String>().map {
                RECORDS_TYPE_NAME_MAP[it] ?: throw IllegalArgumentException("Unexpected RecordType: $it")
            }.toSet()
            val request = ChangesTokenRequest(
                    recordTypes = types,
                    dataOriginFilters = call.data.getDataOriginFilter("dataOriginFilter"),
            )
            val token = healthConnectClient.getChangesToken(request)

            val res = JSObject().apply {
                this.put("token", token)
            }
            call.resolve(res)
        }
    }

    @PluginMethod
    fun getChanges(call: PluginCall) {
        this.activity.lifecycleScope.launch {
            var token = requireNotNull(call.getString("token"))
            val changes = flow {
                do {
                    val result = healthConnectClient.getChanges(
                            changesToken = token,
                    )
                    emit(result.changes)
                    token = result.nextChangesToken
                } while (result.hasMore)
            }.toList().flatten()

            val res = JSObject().apply {
                put("changes", changes.map { it.toJSObject() }.toJSONArray())
                put("nextToken", token)
            }
            call.resolve(res)
        }
    }

    @PluginMethod
    fun requestHealthPermissions(call: PluginCall) {

        if (HealthConnectClient.getSdkStatus(this.context) == HealthConnectClient.SDK_UNAVAILABLE_PROVIDER_UPDATE_REQUIRED) {

            val intent = Intent(Intent.ACTION_VIEW)
            intent.setPackage("com.android.vending")
            intent.data = Uri.parse("market://details")
                    .buildUpon()
                    .appendQueryParameter("id", "com.google.android.apps.healthdata")
                    .appendQueryParameter("url", "healthconnect://onboarding")
                    .build()
            intent.putExtra("overlay", true)
            intent.putExtra("callerId", context.packageName)
            startActivityForResult(call, intent, "handleInstalled")
            return
        }

        val readPermissions = call.getArray("read").toList<String>().map {
            HealthPermission.getReadPermission(
                recordType = RECORDS_TYPE_NAME_MAP[it] ?: throw IllegalArgumentException("Unexpected RecordType: $it")
            )
        }.toSet()

        val writePermissions = call.getArray("write").toList<String>().map {
            HealthPermission.getWritePermission(
                recordType = RECORDS_TYPE_NAME_MAP[it] ?: throw IllegalArgumentException("Unexpected RecordType: $it")
            )
        }.toSet()

        val intent = permissionContract.createIntent(
            this.context,
            readPermissions + writePermissions,
        )
        


        val intent2 = permissionContract.createIntent(
            context = this.context,
            input = setOf(
                HealthPermission.getReadPermission(WeightRecord::class),
                HealthPermission.getWritePermission(WeightRecord::class),
                HealthPermission.getWritePermission(StepsRecord::class),
                HealthPermission.getReadPermission(StepsRecord::class),
                HealthPermission.getReadPermission(HeartRateRecord::class),
                HealthPermission.getWritePermission(HeartRateRecord::class),
                HealthPermission.getReadPermission(SleepSessionRecord::class),
                HealthPermission.getWritePermission(SleepSessionRecord::class),
                HealthPermission.getReadPermission(BloodPressureRecord::class),
                HealthPermission.getWritePermission(BloodPressureRecord::class)
            )
        )

        startActivityForResult(call, intent2, "handleRequestPermission")

    }

    @ActivityCallback
    fun handleInstalled(call: PluginCall, result: ActivityResult) {
        if (result.resultCode == Activity.RESULT_OK) {
            requestHealthPermissions(call)
        } else {
            call.errorCallback("canceled")
        }
    }

    @ActivityCallback
    fun handleRequestPermission(call: PluginCall, result: ActivityResult) {

        val reqReadPermissions = call.getArray("read").toList<String>().map {
            HealthPermission.getReadPermission(
                    recordType = RECORDS_TYPE_NAME_MAP[it] ?: throw IllegalArgumentException("Unexpected RecordType: $it")
            )
        }.toSet()

        val reqWritePermissions = call.getArray("write").toList<String>().map {
            HealthPermission.getWritePermission(
                    recordType = RECORDS_TYPE_NAME_MAP[it] ?: throw IllegalArgumentException("Unexpected RecordType: $it")
            )
        }.toSet()

        val grantedPermissions = permissionContract.parseResult(result.resultCode, result.data).toSet()
        val hasAllPermissions = grantedPermissions.containsAll(reqReadPermissions + reqWritePermissions)
        println(grantedPermissions)
        println(hasAllPermissions)

        val res = JSObject().apply {
            put("grantedPermissions", JSArray(grantedPermissions))
            put("hasAllPermissions", hasAllPermissions)
        }
        call.resolve(res)
    }

    @PluginMethod
    fun checkHealthPermissions(call: PluginCall) {
        this.activity.lifecycleScope.launch {
            val reqReadPermissions = call.getArray("read").toList<String>().map {
                HealthPermission.getReadPermission(
                        recordType = RECORDS_TYPE_NAME_MAP[it]
                                ?: throw IllegalArgumentException("Unexpected RecordType: $it")
                )
            }.toSet()
            val reqWritePermissions = call.getArray("write").toList<String>().map {
                HealthPermission.getWritePermission(
                        recordType = RECORDS_TYPE_NAME_MAP[it]
                                ?: throw IllegalArgumentException("Unexpected RecordType: $it")
                )
            }.toSet()

            val grantedPermissions = healthConnectClient.permissionController.getGrantedPermissions()
            println("[healthconnectPluging] granted Permissions")
            println(grantedPermissions)
            val hasAllPermissions = grantedPermissions.containsAll(reqReadPermissions + reqWritePermissions)
            println("[healthconnectPluging] Has All Permissions")
            println(hasAllPermissions)

            val res = JSObject().apply {
                put("grantedPermissions", JSArray(grantedPermissions))
                put("hasAllPermissions", hasAllPermissions)
            }
            call.resolve(res)
        }
    }

    @PluginMethod
    fun revokeHealthPermissions(call: PluginCall) {
        this.activity.lifecycleScope.launch {
            healthConnectClient.permissionController.revokeAllPermissions()
            call.resolve()
        }
    }

    @PluginMethod
    fun openHealthConnectSetting(call: PluginCall) {
        val action = HealthConnectClient.ACTION_HEALTH_CONNECT_SETTINGS
        val intent = Intent(action)
        this.context.startActivity(intent)

        call.resolve()
    }
}
