import {finalUrl} from "../baseUrl.ts";
import {useAtom} from "jotai";
import {
    AuthorClient,
    BookClient,
    type CreateAuthorRequestDto, type CreateBookRequestDto, type CreateGenreRequestDto,
    GenreClient,
    type UpdateAuthorRequestDto, type UpdateBookRequestDto, type UpdateGenreRequestDto
} from "../generated-ts-client.ts";
import toast from "react-hot-toast";
import {AllAuthorsAtom, AllBooksAtom, AllGenresAtom} from "../atoms/libraryAtoms.ts";
import customcatch from "../errors/customcatch.ts";

const authorApi = new AuthorClient(finalUrl);
const bookApi = new BookClient(finalUrl);
const genreApi = new GenreClient(finalUrl);

export default function useLibraryCrud() {
    const [authors, setAuthors] = useAtom(AllAuthorsAtom);
    const [books, setBooks] = useAtom(AllBooksAtom);
    const [genres, setGenres] = useAtom(AllGenresAtom);

    async function getAuthors() {
        try {
            const result = await authorApi.getAllAuthors();
            setAuthors(result);
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function createAuthor(dto: CreateAuthorRequestDto) {
        try {
            const result = await authorApi.createAuthor(dto);
            const duplicate = [...authors];
            duplicate.push(result);
            setAuthors(duplicate);
            toast.success("Author successfully created");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function updateAuthor(dto: UpdateAuthorRequestDto) {
        try {
            const result = await authorApi.updateAuthor(dto);
            const index = authors.findIndex(a => a.id === result.id);
            if (index > -1) {
                const duplicate = [...authors];
                duplicate[index] = result;
                setAuthors(duplicate);
            }
            result.bookIds.forEach(bookId => {
                const bookIndex = books.findIndex(b => b.id === bookId);
                if (bookIndex > -1) {
                    const bookDuplicate = [...books];
                    if (!bookDuplicate[bookIndex].authorIds.includes(result.id)) {
                        bookDuplicate[bookIndex].authorIds.push(result.id);
                        setBooks(bookDuplicate);
                    }
                }
            })
            toast.success("Author successfully updated");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function deleteAuthor(id: string) {
        try {
            const result = await authorApi.deleteAuthor(id);
            const filtered = authors.filter(a => a.id !== id);
            setAuthors(filtered);
            toast.success("Author successfully deleted");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function getBooks() {
        try {
            const result = await bookApi.getAllBooks();
            setBooks(result);
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function createBook(dto: CreateBookRequestDto) {
        try {
            const result = await bookApi.createBook(dto);
            const duplicate = [...books];
            duplicate.push(result);
            setBooks(duplicate);
            toast.success("Book successfully created");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function updateBook(dto: UpdateBookRequestDto) {
        try {
            const result = await bookApi.updateBook(dto);
            const index = books.findIndex(b => b.id === result.id);
            if (index > -1) {
                const duplicate = [...books];
                duplicate[index] = result;
                setBooks(duplicate);
            }
            result.authorIds.forEach(authorId => {
                const authorIndex = authors.findIndex(a => a.id === authorId);
                if (authorIndex > -1) {
                    const authorDuplicate = [...authors];
                    if(!authorDuplicate[authorIndex].bookIds.includes(result.id)) {
                        authorDuplicate[authorIndex].bookIds.push(result.id);
                        setAuthors(authorDuplicate);
                    }
                }
            })
            const genre = genres.findIndex(g => g.id === result.genreId);
            if (genre > -1) {
                const genreDuplicate = [...genres];
                if(!genreDuplicate[genre].bookIds.includes(result.id)) {
                    genreDuplicate[genre].bookIds.push(result.id);
                    setGenres(genreDuplicate);
                }
            }
            toast.success("Book successfully updated");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function deleteBook(id: string) {
        try {
            const result = await bookApi.deleteBook(id);
            const filtered = books.filter(b => b.id !== id);
            setBooks(filtered);
            toast.success("Book successfully deleted");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function getGenres() {
        try {
            const result = await genreApi.getAllGenres();
            setGenres(result);
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function createGenre(dto: CreateGenreRequestDto) {
        try {
            const result = await genreApi.createGenre(dto);
            const duplicate = [...genres];
            duplicate.push(result);
            setGenres(duplicate);
            toast.success("Genre successfully created");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function updateGenre(dto: UpdateGenreRequestDto) {
        try {
            const result = await genreApi.updateGenre(dto);
            const index = genres.findIndex(g => g.id === result.id);
            if (index > -1) {
                const duplicate = [...genres];
                duplicate[index] = result;
                setGenres(duplicate);
            }
            toast.success("Genre successfully updated");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    async function deleteGenre(id: string) {
        try {
            const result = await genreApi.deleteGenre(id);
            const filtered = genres.filter(g => g.id !== id);
            setGenres(filtered);
            toast.success("Genre successfully deleted");
            return result;
        } catch (e : any) {
            customcatch(e);
        }
    }

    return {
        getAuthors,
        createAuthor,
        updateAuthor,
        deleteAuthor,
        getBooks,
        createBook,
        updateBook,
        deleteBook,
        getGenres,
        createGenre,
        updateGenre,
        deleteGenre
    }
}