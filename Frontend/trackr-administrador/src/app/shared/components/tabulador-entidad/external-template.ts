import { TemplateRef, Type } from "@angular/core";

export class ExternalTemplate {

    component: Type<any>;
    label: string;
    enabled: boolean;
    args: { [key: string]: any };

    externalSubmit?: boolean = false;
    submitControl?: boolean;
    
}