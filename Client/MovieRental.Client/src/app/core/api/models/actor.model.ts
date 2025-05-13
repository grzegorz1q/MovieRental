import { Movie } from "./movie.model";

export interface Actor {
    id: number;
    firstName: string;
    lastName: string;
    movies: Movie[];
}