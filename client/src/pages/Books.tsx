import {useAtom} from "jotai";
import {AllBooksAtom, AllGenresAtom} from "../atoms/libraryAtoms.ts";
import BookCard from "../components/book/BookCard.tsx";
import {useNavigate} from "react-router";
import {useState} from "react";

export default function Books () {

    const [books] = useAtom(AllBooksAtom);
    const [genres] = useAtom(AllGenresAtom);
    const navigate = useNavigate();

    const [searchTerm, setSearchTerm] = useState("");
    const [selectedGenres, setSelectedGenres] = useState<string[]>([]);

    function toggleGenres (genreId: string) {
        setSelectedGenres(prev =>
            prev.includes(genreId) ? prev.filter(id => id !== genreId) : [...prev, genreId]
        )
    }

    const filteredBooks = books.filter(book => {
        const matchesSearch = searchTerm.trim() === ""
            || book.title.toLowerCase().includes(searchTerm.toLowerCase());

        const matchesGenre = selectedGenres.length === 0
            || selectedGenres.includes(book.genreId!);

        return matchesSearch && matchesGenre;
    });

    return (
        <>
            <div className='mb-4 p-4 flex flex-row justify-between items-center'>
        <h1 className='text-2xl font-bold text-primary '>Books</h1>
                <div>
                    <label className="input w-96 flex items-center">
                        <svg
                            className="h-[1em] opacity-50"
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 24 24"
                        >
                            <g
                                strokeLinejoin="round"
                                strokeLinecap="round"
                                strokeWidth="2.5"
                                fill="none"
                                stroke="currentColor"
                            >
                                <circle cx="11" cy="11" r="8"></circle>
                                <path d="m21 21-4.3-4.3"></path>
                            </g>
                        </svg>
                        <input
                            type="search"
                            className="grow"
                            placeholder="Search"
                            value={searchTerm}
                            onChange={e => setSearchTerm(e.target.value)}
                        />
                    </label>
                </div>
                <button className="btn btn-primary" onClick={() => {
                    navigate("/book/create")
                }}>
                    Add Book
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="2.5" stroke="currentColor" className="size-[1.2em]"><path strokeLinecap="round" strokeLinejoin="round" d="M12 4.5v15m7.5-7.5h-15" /></svg>
                </button>
            </div>
            <div className="px-4 mb-6 flex flex-wrap gap-2 items-center">
                {genres.map(genre => (
                        <input
                            type="checkbox"
                            checked={selectedGenres.includes(genre.id)}
                            onChange={() => toggleGenres(genre.id)}
                            className="btn"
                            aria-label={genre.name}
                        />
                ))}
                {selectedGenres.length > 0 && (
                    <button
                        type="button"
                        className="btn btn-square"
                        onClick={() => setSelectedGenres([])}
                    >
                        ×
                    </button>
                )}
            </div>
        <div className='grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 xl:grid-cols-6 gap-6 p-4'>
            {
                filteredBooks.map((book) => {
                    return (
                        <BookCard key={book.id} book={book} />
                    )
                })
            }
        </div>
        </>
    )
}