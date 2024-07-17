import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { File, Bell, ChevronRight, Circle, FileCheck2, Home, LogOut, LucideAngularModule, MessageSquareMore, Search, Settings, Users, Mic, Trash2, Send, CircleStop, BadgeAlert, Video, X, Phone, Plus, Calendar, Check, Download, Pen, Eye, ChevronDown, Tally1, Filter, EyeOff, DollarSign, Stethoscope, BarChart3, Hospital, Building, Store, Receipt, Redo2, Banknote, Earth, Book, Calculator, Monitor, Percent, CirclePercent, Truck, Boxes, Upload, Coins, Network, Syringe, Thermometer, Activity, Pill, PillBottle, Tablets, MessageSquare, ClipboardList, Contact, PackageOpen } from 'lucide-angular';

const iconos = {
  Settings, 
  Home,
  FileCheck2, 
  Users, 
  MessageSquareMore, 
  LogOut, 
  ChevronRight,
  Search, 
  Bell, 
  Circle, 
  File, 
  Mic, 
  Trash2, 
  Send, 
  CircleStop,
  BadgeAlert, 
  Video,
  X, 
  Phone,
  Plus,
  Calendar,
  Check,
  Filter, 
  Tally1, 
  ChevronDown, 
  Eye,
  EyeOff, 
  Pen, 
  Download,
  DollarSign,
  Stethoscope,
  BarChart3,
  Hospital,
  Building,
  Store,
  Receipt,
  Redo2,
  Banknote,
  Earth,
  Book,
  Calculator,
  Monitor,
  Percent,
  CirclePercent,
  Truck,
  Boxes,
  Upload,
  Coins,
  Network,
  Syringe,
  Thermometer,
  Activity,
  Pill,
  PillBottle,
  Tablets,
  MessageSquare,
  ClipboardList,
  Contact,
  PackageOpen
}

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    LucideAngularModule.pick(iconos)
  ]
})
export class LucideIconsModule { }
