import {useState} from "react";
import type {UpdateGenreRequestDto} from "../generated-ts-client.ts";
import useLibraryCrud from "../hooks/useLibraryCrud.ts";
import {useNavigate, useParams} from "react-router";
import {useAtom} from "jotai";
import {AllGenresAtom} from "../atoms/libraryAtoms.ts";

export default function GenreDetails () {
    const libraryCrud = useLibraryCrud();
    const navigate = useNavigate();
    const [genres] = useAtom(AllGenresAtom);

    const params = useParams();
    const genre = genres.find(genre => genre.id === params.genreId);

    const [editGenreForm, setEditGenreForm] = useState<UpdateGenreRequestDto>({
        genreId: genre?.id!,
        name: genre?.name!,
    })

    function handleEdit() {
        libraryCrud.updateGenre(editGenreForm);
        navigate("/genres");
    }

    return (
        <form className='flex justify-center items-center p-4' onSubmit={(e) => {
            e.preventDefault();
            handleEdit();
        }}>
            <div className="card bg-base-100 w-96 shadow-sm">
                <div className="card-body">
                    <h2 className="card-title">Edit genre</h2>
                    <input value={editGenreForm.name} onChange={e => setEditGenreForm({...editGenreForm, name: e.target.value})} type="text" required placeholder="Name" className="input" />
                    <div className="card-actions justify-end">
                        <button type="submit" className="btn btn-primary">Edit genre</button>
                    </div>
                </div>
            </div>
        </form>
    )
}