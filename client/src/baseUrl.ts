const isProduction = import.meta.env.PROD;

const prod = "https://library-project-backend.fly.dev"
const dev = "http://localhost:5001"

export const finalUrl = isProduction ? prod : dev