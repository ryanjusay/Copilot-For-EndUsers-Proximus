import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

console.log("process.env.CODESPACE_NAME: " + JSON.stringify(process.env.CODESPACE_NAME));

const apiUrl = process.env.CODESPACE_NAME ? `https://${process.env.CODESPACE_NAME}-1903.app.github.dev` : "http://localhost:1903";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  define: {
    'API_URL': JSON.stringify(apiUrl),
  },
})
