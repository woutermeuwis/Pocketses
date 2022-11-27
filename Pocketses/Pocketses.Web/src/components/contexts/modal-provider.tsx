import React, {PropsWithChildren, ReactNode, useContext, useState} from 'react';
import ReactModal from 'react-modal';

type ModalContextProps = {
    modal: ReactNode | null,
    closeModal: () => void,
    showModal: (modal: ReactNode) => void
}

const ModalContext = React.createContext<ModalContextProps | undefined>(undefined);

const ModalProvider = (props: PropsWithChildren) => {
    const [modal, setModal] = useState<ReactNode | null>(null);
    const closeModal = () => setModal(null);
    const showModal = (modal: ReactNode) => setModal(modal);

    return (
        <ModalContext.Provider value={{modal, closeModal, showModal}} {...props} />
    )
}

const useModal = () => {
    const ctx = useContext(ModalContext);
    if (ctx === undefined)
        throw Error('useModal must be used within ModalProvider');
    return ctx;
}


export {ModalProvider, useModal};