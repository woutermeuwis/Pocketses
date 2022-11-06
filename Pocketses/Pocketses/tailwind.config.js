module.exports = {
    content: [
        './**/*.{html,cshtml}',
        './**/*.{js,ts}'
    ],
    theme: {
        extend: {},
    },
    variants: {
        extend: {}
    },
    plugins: [],
    safelist: [
        {
            pattern: /./,
            variantns: ['sm', 'md', 'lg', 'xl', '2xl']
        }
    ]
}
