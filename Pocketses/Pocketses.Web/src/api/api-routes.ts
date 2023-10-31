const base = import.meta.env.VITE_BASE_URL

export const apiRoutes = {
    base: base,

    auth: '/authenticate',

    campaigns:  '/campaigns',
    campaign: (id: string) => `/campaigns/${id}`,

    characters: '/characters',
    character: (id: string) => `/characters/${id}`
};
