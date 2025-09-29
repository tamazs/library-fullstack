import {createBrowserRouter} from "react-router";
import Books from "../pages/Books.tsx";
import Layout from "../layout/Layout.tsx";
import BookDetails from "../pages/BookDetails.tsx";
import BookCreate from "../pages/BookCreate.tsx";
import Authors from "../pages/Authors.tsx";
import AuthorDetails from "../pages/AuthorDetails.tsx";
import AuthorCreate from "../pages/AuthorCreate.tsx";
import Genres from "../pages/Genres.tsx";
import GenreDetails from "../pages/GenreDetails.tsx";
import GenreCreate from "../pages/GenreCreate.tsx";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <Layout />,
        children: [
            {index: true, element: <Books />},
            {path: "/book/:bookId", element: <BookDetails />},
            {path: "/book/create", element: <BookCreate />},
            {path: "/authors", element: <Authors />},
            {path: "/author/:authorId", element: <AuthorDetails />},
            {path: "/author/create", element: <AuthorCreate />},
            {path: "/genres", element: <Genres />},
            {path: "/genre/:genreId", element: <GenreDetails />},
            {path: "/genre/create", element: <GenreCreate />}
        ]
    }]
);