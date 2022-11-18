import { PropsWithChildren } from "react";

const PageHeader = (props: PropsWithChildren) => {
    return (
        <header className="bg-white shadow">
            <div className="mx-auto max-w-7xl py-6 px-4 sm:px-6 lg:px-8">
                {props.children}
            </div>
        </header>
    );
}

export default PageHeader;