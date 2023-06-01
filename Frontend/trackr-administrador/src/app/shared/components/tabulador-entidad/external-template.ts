import { TemplateRef } from "@angular/core";

export class ExternalTemplate {

    template: TemplateRef<any>;
    label: string;
    enabled: boolean;

    externalSubmit?: boolean = false;
    submitControl?: boolean;
    
}