import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Observable, forkJoin, of, concat } from 'rxjs';
import { map } from 'rxjs/operators';
import { Book } from '../models/Book.model';
import { BookEntity } from '../models/bookEntity.model';
import { BookMapper } from '../mappers/book.mapper';
import { UpdateBookVM } from '../models/updateBookVM.model';
import { UpdateBookStatusVM } from '../models/updateBookStatusVM.model';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  basePath: string = "http://localhost:51067/book/"
  constructor(private _httpClient: HttpClient) {

  }
  getBooks(): Observable<Book[]> {
    return this._httpClient.get<BookEntity[]>(this.basePath + "list")
      .pipe(
        map((books: BookEntity[]) => {
          return BookMapper.mapToBooks(books);
        })
      )
  }
  saveBooks(books: UpdateBookVM[]){
    return this._httpClient.post<void>(this.basePath + "postlist", books);
  }
  updateStatus(updateStatus: UpdateBookStatusVM){
    return this._httpClient.post<void>(this.basePath + "poststatus", updateStatus);
  }
}
