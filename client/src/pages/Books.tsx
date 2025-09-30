import {useAtom} from "jotai";
import {AllBooksAtom} from "../atoms/libraryAtoms.ts";
import BookCard from "../components/book/BookCard.tsx";
import {useNavigate} from "react-router";

export default function Books () {

    const [books] = useAtom(AllBooksAtom);
    const navigate = useNavigate();

    return (
        <>
            <div className='mb-4 p-4 flex flex-row justify-between items-center'>
        <h1 className='text-2xl font-bold text-primary '>Books</h1>
                <button className="btn btn-primary" onClick={() => {
                    navigate("/book/create")
                }}>
                    Add Book
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="2.5" stroke="currentColor" className="size-[1.2em]"><path strokeLinecap="round" strokeLinejoin="round" d="M12 4.5v15m7.5-7.5h-15" /></svg>
                </button>
            </div>
        <div className='grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 xl:grid-cols-6 gap-6 p-4'>
            {
                books.map((book) => {
                    return (
                        <BookCard key={book.id} book={book} />
                    )
                })
            }
        </div>
        </>
    )
}