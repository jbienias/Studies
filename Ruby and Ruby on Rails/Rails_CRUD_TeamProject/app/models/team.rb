class Team < ApplicationRecord
  has_one_attached :image
  has_many :players
  validates :name, :length => { :in => 3..20 }, :format => { :with => /\A[A-Za-z-. ]+\z/ }
  validate :date_of_founding_in_future?
  validate :team_name_unique?

  private

  def team_name_unique?
    if Team.where.not(id: self.id).exists?(:name=>self.name)
      errors.add("Team with that name", "already exists")
    end
  end

  def date_of_founding_in_future?
   if self.date_of_founding > Date.today
     errors.add("Date of founding", "cannot be in the future")
   end
  end

  def self.search(search)
    where("name LIKE ?", "%#{search}%")
  end

end
