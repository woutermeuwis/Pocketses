import {useState} from "react";

interface NewCharacterProps {
    submit: (name: string) => void
}

const NewCharacterForm = ({submit}: NewCharacterProps) => {
    const [name, setName] = useState('');

    function submitForm(event: any) {
        submit(name);
        event.preventDefault();
    }

    return (
        <>
            <form onSubmit={submitForm}>
                <div className={"shadow sm:overflow-hidden sm:rounded-md"}>
                    <div className={"space-y-6 bg-white px-4 py-5 sm:p-6"}>
                        <div>
                            <label htmlFor={"CharacterName"}
                                   className={"block text-sm font-medium text-gray-700"}>
                                Character Name
                            </label>

                            <div className={"flex mt-1 rounded-md shadow-sm"}>
                                <input type={"text"}
                                       id={"CharacterName"}
                                       placeholder={"Name"}
                                       value={name}
                                       onChange={e => setName(e.target.value)}
                                       className={"block w-full flex-1 rounded-md border-gray-300 focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"}/>
                            </div>
                        </div>
                    </div>
                    <div className={"bg-gray-50 px-4 py-3 text-right sm:px-6"}>
                        <button type={"submit"}
                                className={"inline-flex justify-center rounded-md border border-transparent bg-indigo-600 py-2 px-4 text-sm font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"}>
                            Save
                        </button>
                    </div>
                </div>
            </form>
        </>
    );
}

export default NewCharacterForm;