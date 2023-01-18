import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { ROUTES } from "./constants/routes";
import Customers from "./pages/Customers";
import Header from "./pages/Header";
import Home from "./pages/Home";

export default function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path={ROUTES.ROOT} element={<Header />}>
            <Route index element={<Home />} />

            <Route path={ROUTES.CLIENTES} element={<Customers />} />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </>
  );
}
