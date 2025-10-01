import {useState} from "react";
import type {CreateGenreRequestDto} from "../generated-ts-client.ts";
import useLibraryCrud from "../hooks/useLibraryCrud.ts";
import {useNavigate} from "react-router";

export default function GenreCreate () {
    const libraryCrud = useLibraryCrud();
    const navigate = useNavigate();

    const [createGenreForm, setCreateGenreForm] = useState<CreateGenreRequestDto>({
        name: ""
    })

    function handleCreate() {
        libraryCrud.createGenre(createGenreForm);
        navigate("/genres");
    }

    return (
        <form className='flex justify-center items-center p-4' onSubmit={(e) => {
            e.preventDefault();
            handleCreate();
        }}>
            <div className="card bg-base-100 w-96 shadow-sm">
                <div className="card-body">
                    <h2 className="card-title">Add a genre</h2>
                    <input value={createGenreForm.name} onChange={e => setCreateGenreForm({...createGenreForm, name: e.target.value})} type="text" required placeholder="Name" className="input" />
                    <div className="card-actions justify-end">
                        <button type="submit" className="btn btn-primary">Add genre</button>
                    </div>
                </div>
            </div>
        </form>
    )
}