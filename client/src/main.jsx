import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.jsx";
import { PersonProvider } from "./context/PersonProvider.jsx";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <PersonProvider>
      <App />
    </PersonProvider>
  </StrictMode>,
);
