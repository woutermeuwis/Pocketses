import { Disclosure } from "@headlessui/react";
import { ReactNode } from "react";
import { apiRoutes } from "../../api/apiRoutes";
import { useAuth } from "../../components/contexts/auth-context";
import { useUser } from "../../components/contexts/user-context";

const Application = () => {

    const { logout, http } = useAuth();
    const user = useUser();

    const fetchWeather = () => {
        http.get(apiRoutes.baseUrl + 'weatherforecast')
    }

    const onOpen = (open) => {
        return (
            <>
                <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
                    <div className="flex h-16 items-center justify-between">
                        <div className="flex items-center">
                            
                            <div className="flex-shrink-0">
                                <img className="h-8 w-8"
                                    src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=500" />
                            </div>

                            
                        </div>
                    </div>
                </div>
            </>
        )
    }

    const ApplicationHeader = ({ children }: { children: ReactNode }) => {
        return (
            <header className="bg-white-shadow">
                <div className="mx-auto max-w-7xl py-6 px-4 sm:px-6 lg:px-8">
                    <h1 className="text-3xl font-bold tracking-tight text-gray-900">{children}</h1>
                </div>
            </header>
        );
    }

    const ApplicationContent = ({ children }: { children: ReactNode }) => {
        return (
            <div className="mx-auto max-w-7xl py-6 px-4 sm:px-6 lg:px-8">
                {children}
            </div>
        );
    }

    return (
        <div className="min-h-full">
            <Disclosure as="nav" className="bg-gray-800">
                {onOpen}
            </Disclosure>

            <ApplicationHeader>
                Dashboard
            </ApplicationHeader>

            <ApplicationContent>
                <div className="py-6">
                    <div className="h-96 rounded-lg border-4 border-dashed border-gray-200F" />
                </div>
            </ApplicationContent>

        </div>
    )
}
export default Application;