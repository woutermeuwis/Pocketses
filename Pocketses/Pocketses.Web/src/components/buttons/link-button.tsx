import {Link} from "react-router-dom";
import {ReactNode} from "react";

const LinkButton = ({destination, children}: { destination: string, children: ReactNode }) => {
    return (
        <Link to={destination} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
            {children}
        </Link>
    )
}
export default LinkButton;