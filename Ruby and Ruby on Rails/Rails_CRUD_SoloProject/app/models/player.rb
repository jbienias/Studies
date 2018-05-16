class Player < ApplicationRecord
  validates :name, :length => { :in => 3..20 }, :format => { :with => /\A[^0-9`!@#\$%\^&*+_= ]+\z/, :message => " contains inappropriate character(s)" }
  validates :surname, :length => { :in => 3..20 }, :format => { :with => /\A[^0-9`!@#\$%\^&*+_= ]+\z/, :message => " contains inappropriate character(s)" }
  validates :nickname, :length => { :in => 3..20 }, :format => { :with => /\A[a-zA-Z0-9_]+\z/, :message => " contains inappropriate character(s)" }
  validates :salary, format: { with: /\A\d+(?:\.\d{0,2})?\z/ }, numericality: { greater_than_or_equal_to: 0 }
  validate :date_of_birth_in_future?

  private

  def date_of_birth_in_future?
   if self.date_of_birth > Date.today
     errors.add("Date of birth", "cannot be in the future")
   end
 end
end
