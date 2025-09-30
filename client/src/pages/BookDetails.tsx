import {useState} from "react";
import type {UpdateBookRequestDto} from "../generated-ts-client.ts";
import useLibraryCrud from "../hooks/useLibraryCrud.ts";
import {useNavigate, useParams} from "react-router";
import {useAtom} from "jotai";
import {AllAuthorsAtom, AllBooksAtom, AllGenresAtom} from "../atoms/libraryAtoms.ts";

export default function BookDetails () {
    const libraryCrud = useLibraryCrud();
    const navigate = useNavigate();
    const [books] = useAtom(AllBooksAtom);
    const [authors] = useAtom(AllAuthorsAtom);
    const [genres] = useAtom(AllGenresAtom);

    const params = useParams();
    const book = books.find(book => book.id === params.bookId);

    const [editBookForm, setEditBookForm] = useState<UpdateBookRequestDto>({
        bookId: book?.id!,
        title: book?.title!,
        pages: book?.pages!,
        authorIds: authors.filter(a => book?.authorIds.includes(a.id)).map(a => a.id),
        genreId: book?.genreId!
    })

    function handleEdit() {
        libraryCrud.updateBook(editBookForm);
        navigate("/");
    }

    function toggleAuthor(authorId: string) {
        setEditBookForm(prev => {
            const alreadyIncluded = prev.authorIds.includes(authorId);

            return {
                ...prev,
                authorIds: alreadyIncluded
                    ? prev.authorIds.filter(id => id !== authorId)
                    : [...prev.authorIds, authorId]
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
                    <h2 className="card-title">Add a book</h2>
                    <input value={editBookForm.title} onChange={e => setEditBookForm({...editBookForm, title: e.target.value})} type="text" required placeholder="Title" className="input" />
                    <input value={editBookForm.pages} onChange={e => setEditBookForm({...editBookForm, pages: Number.parseInt(e.target.value)})} type="number" required placeholder="Pages" className="input" />
                    <div>
                        <h3 className="font-semibold mb-2">Authors</h3>
                        <div className="flex flex-col gap-2">
                            {authors.map(author => (
                                <label key={author.id} className="flex items-center gap-2">
                                    <input
                                        type="checkbox"
                                        checked={editBookForm.authorIds.includes(author.id)}
                                        onChange={() => toggleAuthor(author.id)}
                                        className="checkbox"
                                    />
                                    {author.name}
                                </label>
                            ))}
                        </div>
                    </div>
                    <div>
                        <h3 className="font-semibold mb-2">Genre</h3>
                        <div className="flex flex-col gap-2">
                            {genres.map(genre => (
                                <label key={genre.id} className="flex items-center gap-2">
                                    <input
                                        type="radio"
                                        name="genre"
                                        checked={editBookForm.genreId === genre.id}
                                        onChange={() => setEditBookForm({...editBookForm, genreId: genre.id})}
                                        className="radio"
                                    />
                                    {genre.name}
                                </label>
                            ))}
                        </div>
                    </div>
                    <div className="card-actions justify-end">
                        <button type="submit" className="btn btn-primary">Edit book</button>
                    </div>
                </div>
            </div>
        </form>
    )
}