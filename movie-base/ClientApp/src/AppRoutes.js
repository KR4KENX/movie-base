import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { Movies } from "./components/Movies";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
    {
        path: '/movies',
        element: <Movies/>
    }
];

export default AppRoutes;
