interface ElementUI {
    $message(option: IMessage);

    $notify(option: INotification);

    $msgbox(option: IMessageBox);
}

interface IMessageBase {
    duration: any;
    message: any;
    offset: any;
    type: any;
    showClose: any;
}

interface IMessage extends IMessageBase {
    center: any;
}

interface INotification extends IMessageBase {
    title: any;
    position: any;
    onClick: any;
    customClass: string;
}

interface IMessageBox {
    title?: any;
    message?: any;
    type?: any;
    showConfirmButton?: boolean;
    confirmButtonText?: any;
    showCancelButton?: boolean;
    cancelButtonText?: any;
    showClose?: boolean;
    closeOnClickModal?: boolean;
    closeOnPressEscape?: boolean;
    showInput?: boolean;
    inputPlaceholder?: any;
    inputType?: string;
    center?: boolean;
    roundButton?: boolean;
    callback: any;
    distinguishCancelAndClose: boolean;
}
