/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./index.html",
        "./src/**/*.{js,ts,jsx,tsx}"
    ],
    theme: {
        extend: {
            keyframes: {
                'bounce-vertically-90': {
                    '0%, 25%': {transform: 'translateY(0%)'},
                    '10%': {transform: 'translateY(-90%)'}
                }
            },
            animation: {
                'dot-bounce-1': 'bounce-vertically-90 1000ms 100ms infinite ease',
                'dot-bounce-2': 'bounce-vertically-90 1000ms 200ms infinite ease',
                'dot-bounce-3': 'bounce-vertically-90 1000ms 300ms infinite ease',
            },
        },

    },
    plugins: [
        require('@tailwindcss/forms')
    ],
}
