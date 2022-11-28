const LoadingSpinner = () => {
    return (
        <div
            className={"fixed top-0 left-0 right-0 bottom-0 flex flex-col justify-center items-center bg-gray-400 bg-opacity-60 z-50"}>

            <div className={"flex gap-1"}>
                <div className={"animate-dot-bounce-1 w-4 h-4 rounded-full bg-gray-700"}></div>
                <div className={"animate-dot-bounce-2 ma w-4 h-4 rounded-full bg-gray-700"}></div>
                <div className={"animate-dot-bounce-3 w-4 h-4 rounded-full bg-gray-700"}></div>
            </div>

            <p className={"text-2xl font-bold text-gray-700"}>
                Loading...
            </p>

        </div>
    )
}

export default LoadingSpinner;