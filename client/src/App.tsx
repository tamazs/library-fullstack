import {RouterProvider} from "react-router";
import {router} from "./routes/router.tsx";
import useLibraryCrud from "./hooks/useLibraryCrud.ts";
import {useEffect} from "react";

function App() {

    const libraryCrud = useLibraryCrud();

    useEffect(() => {
        libraryCrud.getAuthors();
            libraryCrud.getBooks();
            libraryCrud.getGenres();
    }, [])

  return <RouterProvider router={router} />
}

export default App
