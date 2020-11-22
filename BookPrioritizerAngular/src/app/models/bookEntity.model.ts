import { AuthorEntity } from "./authorEntity.model";
import { StatusEntity } from "./statusEntity.model";

export class BookEntity
{
    bookId: number;
    name: string;
    uniqueName: string;
    goodReadsRating: number;
    goodReadsVotes: number;
    amazonRating: number;
    amazonVotes: number;
    statusId: number;
    dateAdded: string;
    authorId: number;
    year: number;
    author: AuthorEntity;
    status: StatusEntity;

}