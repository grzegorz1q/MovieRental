import { Actor } from "./actor.model";

export interface Movie {
    id: number;
    title: string;
    description: string;
    director: string;
    releaseDate: Date;
    count: number;
    isAvailable: boolean;
    actors: Actor[];
}