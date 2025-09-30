import type {BookDto} from "../../generated-ts-client.ts";
import {useAtom} from "jotai";
import {AllAuthorsAtom, AllGenresAtom} from "../../atoms/libraryAtoms.ts";
import {useNavigate} from "react-router";
import useLibraryCrud from "../../hooks/useLibraryCrud.ts";

interface BookProps {
    book: BookDto
}

export default function BookCard({ book } : BookProps) {
    const [authors] = useAtom(AllAuthorsAtom);
    const [genres] = useAtom(AllGenresAtom);
    const navigate = useNavigate();
    const libraryCrud = useLibraryCrud();

    const getAuthorNames: string[] = authors.filter(a => book.authorIds.includes(a.id) && a.bookIds.includes(book.id)).map(a => a.name);

    const genre = genres.find(g => g.id === book.genreId);
    const getGenreName = genre ? genre.name : "Unknown"

    return (
        <div className="card card-border bg-base-100 w-96 h-50 mb-4">
            <div className="card-body">
                <h2 className="card-title text-primary">{book.title}</h2>
                <p>Genre: {getGenreName}</p>
                <p>Author(s): {getAuthorNames.join(', ')}</p>
                <p>Pages: {book.pages}</p>
                <div className="card-actions justify-end">
                    <button className="btn btn-info" onClick={() => {
                        navigate(`/book/${book.id}`)
                    }}>Edit</button>
                    <button className="btn btn-error" onClick={() => {
                        libraryCrud.deleteBook(book.id);
                    }}>Delete</button>
                </div>
            </div>
        </div>
    )
}