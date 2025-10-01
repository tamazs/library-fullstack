import {useState} from "react";
import type {UpdateAuthorRequestDto} from "../generated-ts-client.ts";
import useLibraryCrud from "../hooks/useLibraryCrud.ts";
import {useNavigate, useParams} from "react-router";
import {useAtom} from "jotai";
import {AllAuthorsAtom, AllBooksAtom} from "../atoms/libraryAtoms.ts";

export default function AuthorDetails () {
    const libraryCrud = useLibraryCrud();
    const navigate = useNavigate();
    const [books] = useAtom(AllBooksAtom);
    const [authors] = useAtom(AllAuthorsAtom);

    const params = useParams();
    const author = authors.find(author => author.id === params.authorId);

    const [editAuthorForm, setEditAuthorForm] = useState<UpdateAuthorRequestDto>({
        authorId: author?.id!,
        name: author?.name!,
        bookIds: books.filter(b => author?.bookIds.includes(b.id)).map(b => b.id)
    })

    function handleEdit() {
        libraryCrud.updateAuthor(editAuthorForm);
        navigate("/authors");
    }

    function toggleBook(bookId: string) {
        setEditAuthorForm(prev => {
            const alreadyIncluded = prev.bookIds.includes(bookId);

            return {
                ...prev,
                bookIds: alreadyIncluded
                    ? prev.bookIds.filter(id => id !== bookId)
                    : [...prev.bookIds, bookId]
            };
        });
    }

    return (
        <form className='flex justify-center items-center p-4' onSubmit={(e) => {
            e.preventDefault();
            handleEdit();
        }}>
            <div className="card bg-base-100 w-96 shadow-sm">
                <div className="card-body">
                    <h2 className="card-title">Edit authors</h2>
                    <input value={editAuthorForm.name} onChange={e => setEditAuthorForm({...editAuthorForm, name: e.target.value})} type="text" required placeholder="Name" className="input" />
                    <div>
                        <h3 className="font-semibold mb-2">Books</h3>
                        <div className="flex flex-col gap-2">
                            {books.map(book => (
                                <label key={book.id} className="flex items-center gap-2">
                                    <input
                                        type="checkbox"
                                        checked={editAuthorForm.bookIds.includes(book.id)}
                                        onChange={() => toggleBook(book.id)}
                                        className="checkbox"
                                    />
                                    {book.title}
                                </label>
                            ))}
                        </div>
                    </div>
                    <div className="card-actions justify-end">
                        <button type="submit" className="btn btn-primary">Edit author</button>
                    </div>
                </div>
            </div>
        </form>
    )
}