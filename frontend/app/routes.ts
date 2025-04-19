import { type RouteConfig, index, route } from "@react-router/dev/routes";

export default [
    index("routes/home.tsx"),
    route("/invoices/:invoice_number", "routes/invoice_details.tsx"),
    route("/invoices/:invoice_number/create_cn", "routes/new_cn.tsx"),
] satisfies RouteConfig;
