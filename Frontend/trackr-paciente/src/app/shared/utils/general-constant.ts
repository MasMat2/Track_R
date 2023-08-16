export class GeneralConstant {
    public static TOKEN_KEY = 'token-paciente';
    public static CLAVE_USUARIO_ADMINISTRADOR = '001';
    public static CLAVE_USUARIO_PACIENTE = '002';


    public static PLACEHOLDER_DROPDOWN = 'Selecciona...';
    public static PLACEHOLDER_DROPDOWN_NO_OPTIONS = 'No se encontraron opciones';
    
  public static CONFIG_DATEPICKER: any = {
    dateInputFormat: 'DD/MM/YYYY',
    showWeekNumbers: false,
    selectFromOtherMonth: true,
    isAnimated: true,
    todayHighlight: true,
    dropdownParent: 'body'
  };

  public static CONFIG_DROPDOWN_DEFAULT: any = {
    dropdownDirection: 'down',
    plugins: {
      remove_button: { title: 'Eliminar' }
    },
    mode: 'multi',
    maxItems: 1
  };

}
