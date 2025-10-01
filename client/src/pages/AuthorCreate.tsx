import {useState} from "react";
import type {CreateAuthorRequestDto} from "../generated-ts-client.ts";
import useLibraryCrud from "../hooks/useLibraryCrud.ts";
import {useNavigate} from "react-router";

export default function AuthorCreate () {
    const libraryCrud = useLibraryCrud();
    const navigate = useNavigate();

    const [createAuthorForm, setCreateAuthorForm] = useState<CreateAuthorRequestDto>({
        name: ""
    })

    function handleCreate() {
        libraryCrud.createAuthor(createAuthorForm);
        navigate("/authors");
    }

    return (
        <form className='flex justify-center items-center p-4' onSubmit={(e) => {
            e.preventDefault();
            handleCreate();
        }}>
            <div className="card bg-base-100 w-96 shadow-sm">
                <div className="card-body">
                    <h2 className="card-title">Add an author</h2>
                    <input value={createAuthorForm.name} onChange={e => setCreateAuthorForm({...createAuthorForm, name: e.target.value})} type="text" required placeholder="Name" className="input" />
                    <div className="card-actions justify-end">
                        <button type="submit" className="btn btn-primary">Add author</button>
                    </div>
                </div>
            </div>
        </form>
    )
}