// import { ModalOptions } from "ngx-bootstrap"

export const BaseModalConfig: any = {
  animated: true,
  keyboard: false,
  backdrop: 'static',
  ignoreBackdropClick: true,
};

export const ModalConfigurations: { [key: string]: any } = {
  DEFAULT: {
    ...BaseModalConfig,
    class: 'modal-md modal-size-md modal-position-center',
  },
  MEDIUM: {
    ...BaseModalConfig,
    class: 'modal-medium modal-size-md modal-position-center',
  },
} as const;

export * as ModalConstants from './modal';
