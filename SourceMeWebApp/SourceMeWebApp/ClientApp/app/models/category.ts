export interface Category{
    id: number;
    name: string;
    isChecked: boolean;
    noOfChannels: number;
    subCategories: Category[];
}