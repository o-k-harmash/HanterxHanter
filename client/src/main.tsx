import { StrictMode } from "react";
import { createRoot } from "react-dom/client";

import { BrowserRouter as Router } from "react-router";

import App from "./App.tsx";

import { store } from "./app/store.ts";
import { Provider } from "react-redux";

import "./primitiveui.css";
import "./index.css";
import "./styles";
import "./layouts/navbar.css";
import "./features/profiles/profiles.css";
import "./features/editProfile/editProfile.css";
import "./features/account/account.css";
import "./features/auth/auth.css";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <Provider store={store}>
      <Router>
        <App />
      </Router>
    </Provider>
  </StrictMode>
);
