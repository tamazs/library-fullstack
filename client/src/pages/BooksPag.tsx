import {useAtom} from "jotai";
import {BooksAtom} from "../atoms/libraryAtoms.ts";
import {useNavigate, useSearchParams} from "react-router";
import {useEffect} from "react";
import useLibraryCrud from "../hooks/useLibraryCrud.ts";


export default function BooksPag() {
    const [pagBooks] = useAtom(BooksAtom);

    const navigate = useNavigate();

    const libraryCrud = useLibraryCrud();

    const [searchParams] = useSearchParams();
    const pageNumber = parseInt(searchParams.get('pageNumber') || '1');
    const itemsPerPage = parseInt(searchParams.get('itemsPerPage') || '2')

    useEffect(() => {
        libraryCrud.getBooksPag(
            (pageNumber-1)*itemsPerPage,
            itemsPerPage
        )
    }, [searchParams]);

    return (
        <>
            {pagBooks.map(b => {
                return (
                    <div key={b.id}>
                        {b.title}
                        {b.authors.map(a => {
                            return <span key={a.id}>{a.name}</span>;
                        })}
                    </div>
                )
            })}
            <button className='btn' onClick={() => {
                const newPageNumber = pageNumber-1;
                navigate('./?itemsPerPage='+itemsPerPage+'&pageNumber='+newPageNumber)
            }}>Prev page</button>
            Page: {
            pageNumber
        }
            <button className='btn' onClick={() => {
                const newPageNumber = pageNumber+1;
                navigate('./?itemsPerPage='+itemsPerPage+'&pageNumber='+newPageNumber)
            }}>Next page</button>
            <input className='input' onChange={e => {
                const newItemsPerPage = Number.parseInt(e.target.value)
                navigate('./?itemsPerPage='+newItemsPerPage+'&pageNumber='+pageNumber)
            }} defaultValue={itemsPerPage} />
        </>
    )
}