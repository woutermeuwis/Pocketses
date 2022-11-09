import { useState } from "react";

function useToken() {
    const getToken = () => localStorage.getItem('token');

    const [token, setToken] = useState(getToken);

    const saveToken = (userToken: string) => {
        localStorage.setItem('token', userToken);
        setToken(userToken);
    }

    const clearToken = ()=>{
        localStorage.removeItem('token');
        setToken(null);
    }

    return {
        setToken: saveToken,
        clearToken: clearToken,
        token
    };
}

export default useToken;