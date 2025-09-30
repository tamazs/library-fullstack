import {useState} from "react";
import type {CreateBookRequestDto} from "../generated-ts-client.ts";
import useLibraryCrud from "../hooks/useLibraryCrud.ts";
import {useNavigate} from "react-router";

export default function BookCreate () {
    const libraryCrud = useLibraryCrud();
    const navigate = useNavigate();

    const [createBookForm, setCreateBookForm] = useState<CreateBookRequestDto>({
        title: "",
        pages: 0
    })

    function handleCreate() {
        libraryCrud.createBook(createBookForm);
        navigate("/");
    }

    return (
        <form className='flex justify-center items-center p-4' onSubmit={(e) => {
            e.preventDefault();
            handleCreate();
        }}>
            <div className="card bg-base-100 w-96 shadow-sm">
                <div className="card-body">
                    <h2 className="card-title">Add a book</h2>
                    <input value={createBookForm.title} onChange={e => setCreateBookForm({...createBookForm, title: e.target.value})} type="text" required placeholder="Title" className="input" />
                    <input value={createBookForm.pages} onChange={e => setCreateBookForm({...createBookForm, pages: Number.parseInt(e.target.value)})} type="number" required placeholder="Pages" className="input" />
                    <div className="card-actions justify-end">
                        <button type="submit" className="btn btn-primary">Add book</button>
                    </div>
                </div>
            </div>
        </form>
    )
}