export interface BookModel{

  /** The unique identifier for the book. */
  id: number;

  /** The title of the book. */
  title: string;

  /** The author of the book. */
  author: string;

  /** The publisher of the book. */
  publisher: string;

  /** The date when the book was published. */
  publicationDate: Date;

  /** The ISBN number of the book. */
  isbn: string;

  /** The edition of the book. */
  edition?: string; // Optional since the original property is not required

  /** The language the book is written in. */
  language: string;

}
