import {atom} from "jotai";

import type {AuthorDto, Book, BookDto, GenreDto} from "../generated-ts-client.ts";

export const AllAuthorsAtom = atom<AuthorDto[]>([]);
export const AllBooksAtom = atom<BookDto[]>([]);
export const AllGenresAtom = atom<GenreDto[]>([]);
export const BooksAtom = atom<Book[]>([]);