import { Book } from "../models/Book.model";

export class CalculateScore
{
    books: Book[];
    ratingWeight: number;
    votersWeight: number;

    constructor(books: Book[], ratingWeight: number, votersWeight: number){
        this.books = books;
    }

    percentRank(array: number[], value: number) {
        if (!array.length){
          return null;
        }
        var larger = array.filter((v: number) => {
          return v > value;
        })
        return (array.length - larger.length) / array.length;
    }

    calculate(){
          var allRatings = this.books.map((book: Book) => book.goodReadsRating);
          var allVoters = this.books.map((book: Book) => book.goodReadsVotes);
      
          for(var book of this.books){
            book.ratingPercentile = this.percentRank(allRatings, book.goodReadsRating) *100;
            book.votersPercentile = this.percentRank(allVoters, book.goodReadsVotes) * 100;
            book.score = ((book.ratingPercentile * this.ratingWeight ) + (book.votersPercentile * this.votersWeight )) / (this.ratingWeight + this.votersWeight);
          }

          return this.books;
    }
}