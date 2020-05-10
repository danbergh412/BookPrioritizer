import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Observable, forkJoin, of, concat } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  basePath: string = "http://localhost:3000/books/"
  constructor(private _httpClient: HttpClient) {

  }
  mapToBook(book: UnmappedBook): Book{
    let mapBook: Book = {
      id: book.id,
      title: book.title,
      authors: book.authors,
      rating: book.rating,
      voters: book.voters,
      relevant: book.relevant,
      created: new Date(book.created),
      ratingPercentile: undefined,
      votersPercentile: undefined,
      score: undefined
    };
    return mapBook;
  }
  mapFromBook(book: Book): UnmappedBook {
    let unmapped: UnmappedBook = {
      id: book.id,
      title: book.title,
      authors: book.authors,
      rating: book.rating,
      voters: book.voters,
      relevant: book.relevant,
      created: book.created.toString()
    };
    return unmapped;
  }
  mapToBooks(books: UnmappedBook[]): Book[] {
    var mapBooks: Book[] = [];

    for(var book of books){
      mapBooks.push(this.mapToBook(book))
    }
    return mapBooks;
  } 


  getBooks(): Observable<Book[]> {
    return this._httpClient.get<UnmappedBook[]>(this.basePath)
      .pipe(
        map((books: UnmappedBook[]) => {
          return this.mapToBooks(books);
        })
      )
  }
  saveBooks(saveBooks: Book[], currentIndex: number = 0): Observable<void> {
    return Observable.create(
      (observer) => {
        this.getBooks()
        .subscribe((allBooks: Book[]) => {
              saveBooks = saveBooks.filter(
                (saveBook: Book) => {
                return !allBooks.some(
                  (allBook: Book) => allBook.title == saveBook.title && allBook.authors == saveBook.authors);
              })
  
              this.loopSaveBooks(saveBooks, () => {
                observer.next();
              });
            })
      }
    );
  }
  loopSaveBooks(saveBooks: Book[], callback: Function, i: number = 0) {
    if (i > saveBooks.length - 1){
      callback();
    }
    else
    {
      setTimeout(() => {
        this.saveBook(saveBooks[i])
        .subscribe(() => {
            this.loopSaveBooks(saveBooks, callback, ++i);
        });
      }, 250)
      
    }
    
  }
  editBook(book: Book) {
    var bookJson = this.mapFromBook(book);
    return this._httpClient.put<void>(this.basePath + book.id, bookJson);
  }
  saveBook(book: Book){
    book.relevant = true;
    book.created = new Date();
    var bookJson = this.mapFromBook(book);

    return this._httpClient.post<void>(this.basePath, bookJson);
  }
}
