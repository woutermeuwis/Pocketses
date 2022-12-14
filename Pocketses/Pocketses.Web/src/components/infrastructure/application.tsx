import {Disclosure, Menu, Transition} from "@headlessui/react";
import {Fragment, PropsWithChildren, ReactNode} from "react";
import {useAuth} from "../contexts/auth-context";
import {useUser} from "../contexts/user-context";
import {Bars3Icon, XMarkIcon, BriefcaseIcon} from '@heroicons/react/24/outline'
import AppRouter from "./app-router";
import {NavLink} from "react-router-dom"
import {useModal} from "../contexts/modal-provider";
import ReactModal from "react-modal";
import {ToastContainer} from "react-toastify";

const Application = () => {

    const {logout} = useAuth();
    const user = useUser();
    const {modal, closeModal} = useModal();

    const navigation = [
        {name: 'Dashboard', href: 'dashboard'},
        {name: 'Campaigns', href: 'campaigns'},
        {name: 'Characters', href: 'characters'}
    ];

    const userNavigation = [
        {name: 'Log out', href: '', func: logout}
    ]

    const ToNavigationItem = ({name, href}: { name: string, href: string }) => {
        return (<NavLink key={name} to={href} className={({isActive}) => isActive
            ? "bg-gray-900 text-white px-3 py-2 rounded-md text-sm font-medium"
            : "text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
        }>{name}</NavLink>);
    }

    const ToNavigationMobileButton = ({name, href}: { name: string, href: string }) => {
        return (<NavLink key={name} to={href} className={({isActive}) => isActive
            ? "bg-gray-900 text-white block px-3 py-2 rounded-md text-base font-medium"
            : "text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
        }>{name}</NavLink>);
    }

    const ToUserNavigationItem = ({name, href, func}: { name: string, href: string, func: () => void }) => {
        return (<Menu.Item key={name}>
            {(args) => args.active
                ? (<a href={href} onClick={func}
                      className="block px-4 py-2 text-sm text-gray-700 bg-gray-100">{name}</a>)
                : (<a href={href} onClick={func} className="block px-4 py-2 text-sm text-gray-700">{name}</a>)
            }
        </Menu.Item>)
    }

    const ToUserNavigationMobileButton = ({name, href, func}: { name: string, href: string, func: () => void }) => {
        return (<Disclosure.Button as="a" key={name} href={href} onClick={func}
                                   className="block rounded-md px-3 py-2 text-base font-medium text-gray-400 hover:bg-gray-700 hover:text-white">
            {name}
        </Disclosure.Button>);
    }

    const UserNavigationTransition = (props: PropsWithChildren) => {
        return (
            <Transition as={Fragment}
                        enter="transition ease-out duration-100"
                        enterFrom="transform opacity-0 scale-95"
                        enterTo="transform opacity-100 scale-100"
                        leave="transition ease-in duration-75"
                        leaveFrom="transform opacity-100 scale-100"
                        leaveTo="transform opacity-0 scale-95">
                {props.children}
            </Transition>
        )
    }

    const Navbar = () => {
        return (
            <>
                <div className="flex items-center">

                    {/* icon */}
                    <div className="flex-shrink-0">
                        <BriefcaseIcon className="h-8 w-8 text-gray-300"/>
                    </div>

                    {/* nav buttons */}
                    <div className="hidden md:block">
                        <div className="ml-10 flex items-baseline space-x-4">
                            {navigation.map(ToNavigationItem)}
                        </div>
                    </div>

                </div>

                <div className="hidden md:block">
                    <div className="ml-4 flex items-center md:ml-6">
                        {/* profile dropdown */}
                        <Menu as="div" className="relative ml-3">
                            <div>
                                <Menu.Button
                                    className="flex items-center max-w-xs rounded-full bg-gray-800 text-sm focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
                                    <img className="h-8 w-8 rounded-full" referrerPolicy="no-referrer"
                                         src={user.image}/>
                                </Menu.Button>
                            </div>

                            <UserNavigationTransition>
                                <Menu.Items
                                    className="absolute right-0 z-10 mt-2 w-48 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
                                    {userNavigation.map(ToUserNavigationItem)}
                                </Menu.Items>
                            </UserNavigationTransition>

                        </Menu>
                    </div>
                </div>
            </>
        );
    }

    const MobileButton = ({open}: { open: boolean }) => {
        return (
            <div className="-mr-2 flex md:hidden">
                <Disclosure.Button
                    className="rounded-md bg-gray-800 p-2 text-gray-400 hover:bg-gray-700 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
                    {open
                        ? (<XMarkIcon className="block h-6 w-6"/>)
                        : (<Bars3Icon className="block h-6 w-6"/>)}
                </Disclosure.Button>
            </div>
        );
    }

    const Modal = () => {
        if (!modal)
            return null;

        return (
            <ReactModal ariaHideApp={false}
                        isOpen={true}
                        onRequestClose={closeModal}
                        className={"w-fit h-fit mx-auto my-20"}>
                {modal}
            </ReactModal>)
    }

    return (
        <div className="min-h-full">
            <Disclosure as="nav" className="bg-gray-800">
                {(args) => (
                    <>
                        <div className="mx-auto px-4 sm:px-6 lg:px-8">
                            <div className="flex h-16 items-center justify-between">
                                <Navbar/>
                                <MobileButton open={args.open}/>
                            </div>
                        </div>

                        <Disclosure.Panel className="md:hidden">
                            <div className="space-y-1 px-2 pt-2 pb-3 sm:px:3">
                                {navigation.map(ToNavigationMobileButton)}
                            </div>
                            <div className="border-t border-gray-700 pt-4 pb-3">
                                <div className="flex items-center px-5">
                                    <div className="flex-shrink-0">
                                        <img className="h-10 w-10 rounded-full" referrerPolicy="no-referrer"
                                             src={user.image}/>
                                    </div>
                                    <div className="ml-3">
                                        <div
                                            className="text-base font-medium leading-none text-white">{user.givenName} {user.familyName}</div>
                                        <div
                                            className="text-sm font-medium leading-none text-gray-400">{user.email}</div>
                                    </div>
                                </div>
                                <div className="mt-3 space-y-1 px-2">
                                    {userNavigation.map(ToUserNavigationMobileButton)}
                                </div>
                            </div>
                        </Disclosure.Panel>
                    </>
                )}
            </Disclosure>

            <AppRouter/>

            <Modal/>

            <ToastContainer position={"top-center"}
                            autoClose={5000}
                            closeOnClick
                            draggable
                            theme={"colored"}/>

        </div>
    )
}
export default Application;