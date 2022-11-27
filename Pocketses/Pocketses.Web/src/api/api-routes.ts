const base = import.meta.env.VITE_BASE_URL

export const apiRoutes = {
    base: base,
    
    auth: '/authenticate',

    campaigns: '/campaigns'
};