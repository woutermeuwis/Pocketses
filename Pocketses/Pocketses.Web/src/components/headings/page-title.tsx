import { PropsWithChildren } from "react"

const PageTitle = (props: PropsWithChildren) => {
    return (
        <h1 className="text-3xl font-bold tracking-tight text-gray-900">
            {props.children}
        </h1>
    )
}
export default PageTitle;