require 'difference_of_squares'

describe '#square_of_sum' do
  it 'returns 1 for 1' do
    expect(Squares.new(1).square_of_sum).to eq 1
  end

  it 'returns 225 for 5' do
    expect(Squares.new(5).square_of_sum).to eq 225
  end

  it 'returns 25_502_500 for 100' do
    expect(Squares.new(100).square_of_sum).to eq 25_502_500
  end
end

describe '#sum_of_squares' do
  it 'returns 1 for 1' do
    expect(Squares.new(1).sum_of_squares).to eq 1
  end

  it 'returns 55 for 5' do
    expect(Squares.new(5).sum_of_squares).to eq 55
  end

  it 'returns 338_350 for 100' do
    expect(Squares.new(100).sum_of_squares).to eq 338_350
  end
end

describe '#difference_of_squares' do
  it 'returns 0 for 1' do
    expect(Squares.new(1).difference).to eq 0
  end

  it 'returns 170 for 5' do
    expect(Squares.new(5).difference).to eq 170
  end

  it 'returns 25_164_150 for 100' do
    expect(Squares.new(100).difference).to eq 25_164_150
  end
end
