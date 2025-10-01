import {useAtom} from "jotai";
import {AllAuthorsAtom} from "../atoms/libraryAtoms.ts";
import {useNavigate} from "react-router";
import {useState} from "react";
import AuthorCard from "../components/book/AuthorCard.tsx";

export default function Authors () {

    const [authors] = useAtom(AllAuthorsAtom);

    const navigate = useNavigate();

    const [searchTerm, setSearchTerm] = useState("");

    const filteredAuthors = authors.filter(author => {
        return searchTerm.trim() === ""
            || author.name.toLowerCase().includes(searchTerm.toLowerCase());
    });

    return (
        <>
            <div className='mb-4 p-4 flex flex-row justify-between items-center'>
                <h1 className='text-2xl font-bold text-primary '>Authors</h1>
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
                    navigate("/author/create")
                }}>
                    Add Author
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="2.5" stroke="currentColor" className="size-[1.2em]"><path strokeLinecap="round" strokeLinejoin="round" d="M12 4.5v15m7.5-7.5h-15" /></svg>
                </button>
            </div>
            <div className='grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 xl:grid-cols-6 gap-6 p-4'>
                {
                    filteredAuthors.map((author) => {
                        return (
                            <AuthorCard key={author.id} author={author} />
                        )
                    })
                }
            </div>
        </>
    )
}