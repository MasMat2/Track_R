import { ModalOptions } from 'ngx-bootstrap/modal';

export const BASE_MODAL_CONFIG: ModalOptions = {
  animated: true,
  keyboard: false,
  backdrop: 'static',
  ignoreBackdropClick: true,
};

type ModalConfiguration = {
  [key: string]: ModalOptions;
} & {
  Default: ModalOptions;
  Small: ModalOptions;
  Medium: ModalOptions;
  Large: ModalOptions;
  Full: ModalOptions;
};

export const MODAL_CONFIG: ModalConfiguration = {
  Default: {
    ...BASE_MODAL_CONFIG,
    class: 'modal-md modal-size-md modal-position-center',
  },
  Small: {
    ...BASE_MODAL_CONFIG,
    class: 'modal-xs modal-size-xs modal-position-center',
  },
  Medium: {
    ...BASE_MODAL_CONFIG,
    class: 'modal-medium modal-size-md modal-position-center',
  },
  Large: {
    ...BASE_MODAL_CONFIG,
    class: 'modal-xlg modal-size-md modal-position-center dual-modal',
  },
  Full: {
    ...BASE_MODAL_CONFIG,
    class: 'modal-xxlg modal-size-lg modal-position-center',
  },
} as const;

export * as ModalConstants from './modal';
