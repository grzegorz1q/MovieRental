import { Actor } from "./actor.model";

export interface Movie {
    id?: number;
    title: string;
    image: string;
    genre: string;
    description: string;
    director: string;
    releaseDate: Date;
    count: number;
    isAvailable: boolean;
    actors: Actor[];
}