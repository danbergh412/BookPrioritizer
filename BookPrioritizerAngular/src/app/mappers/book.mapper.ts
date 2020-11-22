import { BookEntity } from "../models/bookEntity.model";
import { Book } from "../models/Book.model";
import { UpdateBookStatusVM } from "../models/updateBookStatusVM.model";

export class BookMapper
{
    static mapToBook(book: BookEntity): Book{
        console.log(book);
        let mapBook: Book = {
          name: book.name,
          dateAdded: new Date(book.dateAdded),
          goodReadsRating: book.goodReadsRating,
          goodReadsVotes: book.goodReadsVotes,
          amazonRating: book.amazonRating,
          amazonVotes: book.amazonVotes,
          year: book.year,
          bookId: book.bookId,
          authorId: book.authorId,
          authorLastFirstName: book.author.lastName + ", " + book.author.firstName,
          authorFirstLastName: book.author.firstName + " " + book.author.lastName,
          authorFirstName: book.author.firstName,
          authorLastName: book.author.lastName,
          statusId: book.status.statusId,
          statusName: book.status.name,
          hidden: this.isHidden(book.statusId),
          ratingPercentile: undefined,
          votersPercentile: undefined,
          score: undefined,
          isEditing: false
        };
        return mapBook;
      }
      static toggleHiddenVM(book: Book) {
        book.statusId = book.statusId == 5? 3: 5;
        book.hidden = this.isHidden(book.statusId);
        var updateStatus: UpdateBookStatusVM = {
          bookId: book.bookId,
          statusId: book.statusId
        }
        return updateStatus;
      }
      static isHidden(statusId){
        return statusId == 5? true: false;
      }
      static mapToBooks(books: BookEntity[]): Book[] {
        var mapBooks: Book[] = [];
    
        for(var book of books){
          mapBooks.push(this.mapToBook(book))
        }
        return mapBooks;
      } 
}