import { PropsWithChildren, ReactNode } from "react";

const LoginHeading = (props: PropsWithChildren) => {
    return (
        <h1 className="text-4xl font-bold uppercase pb-8 text-center">
            {props.children}
        </h1>
    )
}
export default LoginHeading;