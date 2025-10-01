import type {AuthorDto} from "../../generated-ts-client.ts";
import {useAtom} from "jotai";
import {AllBooksAtom} from "../../atoms/libraryAtoms.ts";
import {useNavigate} from "react-router";
import useLibraryCrud from "../../hooks/useLibraryCrud.ts";

interface AuthorProps {
    author: AuthorDto
}

export default function AuthorCard({ author } : AuthorProps) {
    const [books] = useAtom(AllBooksAtom);
    const navigate = useNavigate();
    const libraryCrud = useLibraryCrud();

    const getBookNames: string[] = books.filter(b => author.bookIds.includes(b.id) && b.authorIds.includes(author.id)).map(b => b.title);

    return (
        <div className="card card-border bg-base-100 w-96 h-50 mb-4">
            <div className="card-body">
                <h2 className="card-title text-primary">{author.name}</h2>
                <p>Book(s): {getBookNames.join(', ')}</p>
                <div className="card-actions justify-end">
                    <button className="btn btn-info" onClick={() => {
                        navigate(`/author/${author.id}`)
                    }}>Edit</button>
                    <button className="btn btn-error" onClick={() => {
                        libraryCrud.deleteAuthor(author.id);
                    }}>Delete</button>
                </div>
            </div>
        </div>
    )
}