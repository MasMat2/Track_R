export interface CustomAlertData{
    header: string;
    subHeader: string;
    Icono: 'check' | 'info' | 'pill' | 'trash';
    Color: 'primary' | 'error';
    twoButtons: boolean;
    cancelButtonText: string;
    confirmButtonText: string;
    image?: string;
}