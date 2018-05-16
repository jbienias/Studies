class Player < ApplicationRecord
  belongs_to :team, optional: true
  validates :name, :length => { :in => 3..20 }, :format => { :with => /\A[^0-9`!@#\$%\^&*+_= ]+\z/, :message => " contains inappropriate character(s)" }
  validates :surname, :length => { :in => 3..20 }, :format => { :with => /\A[^0-9`!@#\$%\^&*+_= ]+\z/, :message => " contains inappropriate character(s)" }
  validates :nickname, :length => { :in => 3..20 }, :format => { :with => /\A[a-zA-Z0-9_]+\z/, :message => " contains inappropriate character(s)" }
  validates :salary, format: { with: /\A\d+(?:\.\d{0,2})?\z/ }, numericality: { greater_than_or_equal_to: 0 }
  validate :date_of_birth_in_future?
  validate :nickname_unique?
  validate :at_least_twelve?

  private

  def nickname_unique?
    if Player.where.not(id: self.id).exists?(:nickname=>self.nickname)
      errors.add("Player with that nickname", "already exists")
    end
  end

  def date_of_birth_in_future?
   if self.date_of_birth > Date.today
     errors.add("Date of birth", "cannot be in the future")
   end
  end

  def age_in_completed_years (bd, d)
    a = d.year - bd.year
    a = a - 1 if ( bd.month >  d.month or
        (bd.month >= d.month and bd.day > d.day))
    a
  end

  def at_least_twelve?
   if age_in_completed_years(self.date_of_birth, Date.today) < 12
     errors.add("Player must be at least", "12 years old")
   end
  end

  def self.search(search)
    where("name LIKE ?", "%#{search}%")
  end

end
